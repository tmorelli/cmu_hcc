-----------------------------------------------------------------------------------------
--
-- main.lua
--
-----------------------------------------------------------------------------------------

letter = 1
debugPosition = 1

numTouches = 0
touchTimer = nil
modeSwitchTimer = nil

word = " "
angle = {}
angle.x = 0.0
angle.y = 0.0
angle.z = 0.0

target = {}
target.x = 0.0
target.y = 0.0
target.z = 0.0


message = "HELLO"


debounce = 0.2
timeout = 1000

playingVibration = false
pulsesRemaining = 0

--When True This means vibrating to the user
displayingMessage = true



accels = {{0.018,1,0.02},
		  {0.027,0.9,-.426},
		  {0.038,-0.012,-1},
		  {0.17,-0.8286,-.541},
		  {0.1,-1,0.04},
		  {0.0561,-.75,0.654},
		  {0.014,-0.026,1},
		  {0.1755,.8368,.52}}


function getLetter()
	if (letter == 1) then
		return "H"
	end
	if (letter == 2) then
		return "E"
	end
	if (letter == 2) then
		return "L"
	end
	if (letter == 3) then
		return "L"
	end
	if (letter == 4) then
		return "O"
	end
end



function incrementLetter()
	letter = letter +1
	if (letter > string.len(message)) then
		letter = 1
	end
	targetCharacter = string.sub(message,letter,letter)
	
end

function incrementDebugPosition()
	debugPosition = debugPosition + 1
	if (debugPosition > 8) then
		debugPosition = 1
	end
end


function clearVibrationFlag()
	playingVibration = false
end

function pulseVibration()
		system.vibrate()
		pulsesRemaining = pulsesRemaining - 1
		if (pulsesRemaining > 0) then
			timer.performWithDelay(500,pulseVibration)
		else
			timer.performWithDelay(2000,clearVibrationFlag)
		end
end


function playVibrationSequence(numPulses)
	if (playingVibration == true) then
		return;
	else
		pulsesRemaining = numPulses
	end
	
	playingVibration = true

	pulseVibration()
end


function displayTouch(event)
	if (event.phase == "began") then
		incrementLetter()
		
		targetCharacter = string.sub(message,letter,letter)
		targetPosition,targetPulseLength = convertCharacterToOrientationAndIndex(targetCharacter)
		targetPosition = targetPosition
		targetPulseLength = targetPulseLength + 1

		txtCharacter.text = targetCharacter
		txtPosition.text = targetPosition+1
		txtIndex.text = targetPulseLength

--		playVibrationSequence(letter)
	end
end

function touchFinished()
	print (numTouches)
	tx.text = "x: "..angle.x
	ty.text = "y: "..angle.y
	tz.text = "z: "..angle.z


	touchTimer = nil
	orientation = getOrientation()
--	orientation = letter
	nextLetter = getLetter(numTouches, orientation)
	word = word .. nextLetter
	txtWord.text = word
	numTouches = 0
	--word = " "
	if (letter > 7) then
		letter = 0
	end
end

function getOrientation()
	for x=1,8 do
		if ((angle.x > accels[x][1]- debounce and angle.x < accels[x][1]+debounce) and 
			(angle.y > accels[x][2]-debounce and angle.y < accels[x][2]+debounce) and
			(angle.z > accels[x][3]-debounce and angle.z < accels[x][3]+debounce)) then
			return x-1
		end
	end
	return -1
end

function getLetter(numTouches, orientation)
	if (orientation == 0) then
		if (numTouches == 1) then
			return "1"
		elseif (numTouches == 2) then
			return "2"
		elseif (numTouches == 3) then
			return "3"
		elseif (numTouches == 4) then
			return "4"
		elseif (numTouches == 5) then
			return "5"
		elseif (numTouches == 6) then
			return "6"
		elseif (numTouches == 7) then
			return "7"
		elseif (numTouches == 8) then
			return "8"
		elseif (numTouches == 9) then
			return "9"
		elseif (numTouches == 10) then
			return "0"
		end
	end
	if (orientation == 1) then
		if (numTouches == 1) then
			return "A"
		elseif (numTouches == 2) then
			return "B"
		elseif (numTouches == 3) then
			return "C"
		elseif (numTouches == 4) then
			return "D"
		end
	end
	if (orientation == 2) then
		if (numTouches == 1) then
			return "E"
		elseif (numTouches == 2) then
			return "F"
		elseif (numTouches == 3) then
			return "G"
		elseif (numTouches == 4) then
			return "H"
		end
	end
	if (orientation == 3) then
		if (numTouches == 1) then
			return "I"
		elseif (numTouches == 2) then
			return "J"
		elseif (numTouches == 3) then
			return "K"
		elseif (numTouches == 4) then
			return "L"
		end
	end

	if (orientation == 4) then
		if (numTouches == 1) then
			return "M"
		elseif (numTouches == 2) then
			return "N"
		elseif (numTouches == 3) then
			return "O"
		elseif (numTouches == 4) then
			return "P"
		end
	end

	if (orientation == 5) then
		if (numTouches == 1) then
			return "Q"
		elseif (numTouches == 2) then
			return "R"
		elseif (numTouches == 3) then
			return "S"
		elseif (numTouches == 4) then
			return "T"
		end
	end

	if (orientation == 6) then
		if (numTouches == 1) then
			return "U"
		elseif (numTouches == 2) then
			return "V"
		elseif (numTouches == 3) then
			return "W"
		elseif (numTouches == 4) then
			return "X"
		end
	end

	if (orientation == 7) then
		if (numTouches == 1) then
			return "Y"
		elseif (numTouches == 2) then
			return "Z"
		elseif (numTouches == 3) then
			return "."
		end
	end

	return "?"
end



function convertCharacterToOrientationAndIndex(char)
	orientation = -1
	index = -1
	byte = string.byte(char) - 65  --A is at 65
	index = byte % 4 
	orientation = math.floor(byte / 4)
	orientation = orientation + 1

	return orientation,index

end


function readMessage(event)
	if (event.phase == "began") then
		if (touchTimer ~= nil) then
			timer.cancel(touchTimer)
		end
		numTouches = numTouches + 1
		touchTimer = timer.performWithDelay(timeout, touchFinished)		
	end

end

function switchModes()
	if (displayingMessage == true) then
		displayingMessage = false
		txtMode.text = "touch"
	else
		displayingMessage = true
		txtMode.text = "vibrate"
	end
	modeSwitchTimer = nil
end


function handleTouch(event)
	if (event.phase == "began") then
		modeSwitchTimer = timer.performWithDelay(5000, switchModes)
	elseif (event.phase == "ended") then
		if (modeSwitchTimer ~= nil) then
			timer.cancel(modeSwitchTimer)
		end
	end

	if (displayingMessage) then
		displayTouch(event)
	else
		readMessage(event)
	end
	

end

function getAccelValues(o)
	return accels[o+1][1],accels[o+1][2],accels[o+1][3]

end


function orientationMatchesTarget()
	if (getOrientation() == targetPosition) then
		return true
	end
	return false
end



function handleAccelerometerChange(event)
--	tx.text = "x: "..event.xGravity;
--	ty.text = "y: "..event.yGravity;
--	tz.text = "z: "..event.zGravity;

	angle.x = event.xGravity
	angle.y = event.yGravity
	angle.z = event.zGravity
	
--[[	
	if ((event.xGravity > .0 and xGravity < .05) and
	    (event.yGravity > .95 and yGravity < 1.02) and
	    (event.zGravity > .0 and zGravity < .05)) then
	    
			(x=0.05,y=1.00,z=-0.00)--device is pointing down...    
	end
--]]	
--[[	
	if ((event.xGravity > .0 and xGravity < .05) and
	    (event.yGravity > .75 and yGravity < .8) and
	    (event.zGravity > -0.5 and zGravity < -0.35)) then
	    
			(x=0,y=.84,z=-.55)--device is pointing down and left...    
	end
--]]
--[[	
	if ((event.xGravity > .0 and xGravity < .05) and
	    (event.yGravity > .0 and yGravity < .03) and
	    (event.zGravity > -1 and zGravity < -0.90)) then
	    
			(x=-.02,y=0,z=-1)--device is pointing left...    
	end
--]]
--[[	
	if ((event.xGravity > .0 and xGravity < .05) and
	    (event.yGravity > -0.58 and yGravity < -0.55) and
	    (event.zGravity > -0.83 and zGravity < -0.76)) then
	    
			(x=0.02,y=-0.62,z=-0.74)--device is pointing left and up...    
	end
--]]
--[[	
	if ((event.xGravity > .05 and xGravity < .08) and
	    (event.yGravity > -1.01 and yGravity < -0.975) and
	    (event.zGravity > -0.1 and zGravity < .005)) then
	    
			(x=0,y=-0.99,z=-0.49)--device is pointing up...    
	end
--]]
--[[	
	if ((event.xGravity > .0 and xGravity < .05) and
	    (event.yGravity > .95 and yGravity < 1.0) and
	    (event.zGravity > .3 and zGravity < .35)) then
	    
			(x=0.03,y=-.74,z=.66)--device is pointing up and right...    
	end
--]]
--[[	
	if ((event.xGravity > .0 and xGravity < .19) and
	    (event.yGravity > -.15 and yGravity < -.02) and
	    (event.zGravity > 1 and zGravity < 1.05)) then
	    
			(x=0,y=.06,z=1)--device is pointing right...    
	end
--]]
--[[	
	if ((event.xGravity > .0 and xGravity < .03) and
	    (event.yGravity > .75 and yGravity < .90) and
	    (event.zGravity > .65 and zGravity < .75)) then
	    
			(x=0,y=.83,z=.65)--device is pointing right and down...    
	end
--]]

--[[
	if (event.yGravity <0) then
		system.vibrate()
	end
--]]
	if (displayingMessage) then
		if (orientationMatchesTarget()) then
			playVibrationSequence(targetPulseLength)
		end
	
	end
end

tx=display.newText("x",100,100);
ty=display.newText("y",100,200);
tz=display.newText("z",100,300);
txtWord = display.newText(" ", 200,20)

txtCharacter = display.newText("xxx", 200,100)
txtPosition = display.newText("yyy",200,150)
txtIndex = display.newText("zzz", 200,200)
txtMode = display.newText("vibrate", 200,250)

targetCharacter = string.sub(message,letter,letter)
targetPosition,targetPulseLength = convertCharacterToOrientationAndIndex(targetCharacter)
targetPosition = targetPosition 
targetPulseLength = targetPulseLength + 1

txtCharacter.text = targetCharacter
txtPosition.text = targetPosition+1
txtIndex.text = targetPulseLength




system.setAccelerometerInterval( 20 )
Runtime:addEventListener( "accelerometer", handleAccelerometerChange )
Runtime:addEventListener("touch", handleTouch)


target.x,target.y,target.z = getAccelValues(orientation)


-- Your code here

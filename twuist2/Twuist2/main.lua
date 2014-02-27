-----------------------------------------------------------------------------------------
--
-- main.lua
--
-----------------------------------------------------------------------------------------

letter = 1
numTouches = 0
touchTimer = nil
word = " "
angle = {}
angle.x = 0.0
angle.y = 0.0
angle.z = 0.0

debounce = 0.2
timeout = 1000

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
	if (letter > 4) then
		letter = 1
	end
end


function displayTouch(event)
	if (event.phase == "began") then
		incrementLetter()
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
	if ((angle.x > 0.018- debounce and angle.x < 0.018+debounce) and 
		(angle.y > 1-debounce and angle.y < 1+debounce) and
		(angle.z > 0.02-debounce and angle.z < 0.02+debounce)) then
		return 0
	end


	if ((angle.x > 0.027-debounce and angle.x < 0.027+debounce) and 
		(angle.y > 0.9-debounce and angle.y < 0.9+debounce) and
		(angle.z > -.426-debounce and angle.z < -.426+debounce)) then
		return 1
	end

	if ((angle.x > 0.038-debounce and angle.x < 0.038+debounce) and 
		(angle.y > -0.012-debounce and angle.y < -0.012+debounce) and
		(angle.z > -1-debounce and angle.z < -1+debounce)) then
		return 2
	end
	if ((angle.x > 0.17-debounce and angle.x < 0.17+debounce) and 
		(angle.y > -0.8286-debounce and angle.y < -0.8286+debounce) and
		(angle.z > -.541-debounce and angle.z < -.541+debounce)) then
		return 3
	end

	if ((angle.x > 0.1-debounce and angle.x < 0.1+debounce) and 
		(angle.y > -1-debounce and angle.y < -1+debounce) and
		(angle.z > 0.04-debounce and angle.z < 0.04+debounce) )then
		return 4
	end
	if ((angle.x > 0.0561-debounce and angle.x < 0.0561+debounce) and 
		(angle.y > -.75-debounce and angle.y < -.75+debounce) and
		(angle.z > 0.654-debounce and angle.z < 0.654+debounce) )then
		return 5
		
	end
	if ((angle.x > 0.014-debounce and angle.x < 0.014+debounce) and 
		(angle.y > -0.026-debounce and angle.y < -0.026+debounce) and
		(angle.z > 1-debounce and angle.z < 1+debounce)) then
		return 6
	end
	if ((angle.x > 0.1755-debounce and angle.x < 0.1755+debounce) and 
		(angle.y > .8368-debounce and angle.y < .8368+debounce) and
		(angle.z > .52-debounce and angle.z < .52+debounce)) then
		return 7
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




function readMessage(event)
	if (event.phase == "began") then
		if (touchTimer ~= nil) then
			timer.cancel(touchTimer)
		end
		numTouches = numTouches + 1
		touchTimer = timer.performWithDelay(timeout, touchFinished)		
	end

end

function handleTouch(event)
	if (displayingMessage) then
		displayTouch(event)
	else
		readMessage(event)
	end
	

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
end

tx=display.newText("x",100,100);
ty=display.newText("y",100,200);
tz=display.newText("z",100,300);
txtWord = display.newText(" ", 200,20)

system.setAccelerometerInterval( 20 )
Runtime:addEventListener( "accelerometer", handleAccelerometerChange )
Runtime:addEventListener("touch", handleTouch)

-- Your code here

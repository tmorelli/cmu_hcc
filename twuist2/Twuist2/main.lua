-----------------------------------------------------------------------------------------
--
-- main.lua
--
-----------------------------------------------------------------------------------------

letter = 1

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



function handleAccelerometerChange(event)
	if (event.phase == "began") then
		incrementLetter()
	end
end


function handleAccelerometerChange(event)
	tx.text = "x: "..event.xGravity;
	ty.text = "y: "..event.yGravity;
	tz.text = "z: "..event.zGravity;


	
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


system.setAccelerometerInterval( 20 )
Runtime:addEventListener( "accelerometer", handleAccelerometerChange )
Runtime:addEventListener("touch", handleTouch)

-- Your code here

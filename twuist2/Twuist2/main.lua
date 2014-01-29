-----------------------------------------------------------------------------------------
--
-- main.lua
--
-----------------------------------------------------------------------------------------

function handleAccelerometerChange(event)
	tx.text = "x: "..event.xGravity;
	ty.text = "y: "..event.yGravity;
	tz.text = "z: "..event.zGravity;
	
--[[	
	if ((event.xGravity > .7 and xGravity < .8) and
	    (event.yGravity > .95 and yGravity < 1.0) and
	    (event.zGravity > .3 and zGravity < .35)) then
	    
			--device is pointing down...    
	end 
--]]	
	if (event.yGravity <0) then
		system.vibrate()
	end

end

tx=display.newText("x",100,100);
ty=display.newText("y",100,200);
tz=display.newText("z",100,300);


system.setAccelerometerInterval( 20 )
Runtime:addEventListener( "accelerometer", handleAccelerometerChange )

-- Your code here
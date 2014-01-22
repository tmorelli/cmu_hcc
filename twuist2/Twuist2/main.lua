-----------------------------------------------------------------------------------------
--
-- main.lua
--
-----------------------------------------------------------------------------------------

function handleAccelerometerChange(event)
	tx.text = "x: "..event.xGravity;
	ty.text = "y: "..event.yGravity;
	tz.text = "z: "..event.zGravity;

end

tx=display.newText("x",100,100);
ty=display.newText("y",100,200);
tz=display.newText("z",100,300);


system.setAccelerometerInterval( 20 )
Runtime:addEventListener( "accelerometer", handleAccelerometerChange )

-- Your code here
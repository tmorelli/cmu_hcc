-----------------------------------------------------------------------------------------
--
-- main.lua
--
-----------------------------------------------------------------------------------------

-- Your code here

touchDownTime = 0
touchUpTime = 0
lastTouchTime = 0
touchTime = 0
letter = {}
letterBeeps = 0
characterTimeout = nil

A = {0,1}
B = {1,0,0,0}
C = {1,0,1,0}
D = {1,0,0}
E = {0}
F = {0,0,1,0}
G = {1,1,0}
H = {0,0,0,0}
I = {0,0}
J = {0,1,1,1}
K = {1,0,1}
L = {0,1,0,0}
M = {1,1}
N = {1,0}
O = {1,1,1}
P = {0,1,1,0}
Q = {1,1,0,1}
R = {0,1,0}
S = {0,0,0}
T = {1}
U = {0,0,1}
V = {0,0,0,1}
W = {0,1,1}
X = {1,0,0,1}
Y = {1,0,1,1}
Z = {1,1,0,0}

alphabet = {A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z}
characters = {"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"}
word = ""
displayText = nil

function convertBeepsToCharacter()
	for index,value in ipairs(alphabet) do 
--		print (index,#value)
		match = 1
		if (#value == letterBeeps) then
			for i2,v2 in ipairs(value) do
				if (v2 ~= letter[i2]) then
					match = 0
				end
			end
			if (match == 1) then
				word = word .. characters[index]
				displayText.text = word
			end
		end
	end
end

function handleCharacterTimeout(event)
--[[

	print ("Character Timeout")
	for i=1,letterBeeps do
		print(letter[i])
	end
	
--]]
	convertBeepsToCharacter()
	letterBeeps = 0
end

function handleTouch(event)
	if (characterTimeout ~= nil) then
		timer.cancel(characterTimeout)
	end
	if (event.phase == "began") then
		touchDownTime = system.getTimer()
	elseif (event.phase == "ended") then
		lastTouchTime = system.getTimer()
		touchUpTime = lastTouchTime
		touchTime = touchUpTime - touchDownTime
		if (touchTime > 500) then
			letterBeeps = letterBeeps + 1
			letter[letterBeeps] = 1
			print ("Long Touch")
		else
			letterBeeps = letterBeeps + 1
			letter[letterBeeps] = 0
			print ("Short Touch")
		end
		
		characterTimeout = timer.performWithDelay(1000,handleCharacterTimeout)
	end
end

displayText = display.newText( "code", 100, 200, native.systemFontBold, 12 )
Runtime:addEventListener("touch", handleTouch)
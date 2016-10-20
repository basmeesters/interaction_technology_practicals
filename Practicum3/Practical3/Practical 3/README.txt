Practical 3 

Students:
	Eline Bijkerk				3381870
	Jonatan Felipez Queijo		3664856
	Bas Meesters				3700569

Manual:
	When the application is started the user is in the drumming window. The user can use the Wiimote to play the drums.
	There are some issues with the position so it you cannot hover over the whole screen. Whenever the user clicks the
	button the state will change into the drawing state. The user can draw using the Wiimote in this window. The cursor 
	is for unknown reasons a bit off so you draw a bit right beneath it. When the A button is clicked the user will start
	drawing. Whenever the B button is pressed he will stop drawing. We ourselves have had a lot of issues with the sensor -
	bar not been found and the cursor would go off the screen and draw a big line. Therefore whenever this happens the 
	cursor will stop drawing and the user has to press A again to start drawing. The drawing works best when you are quite far 
	from the sensor bar. Also the thinkness of the drawingline will be dependent of the distance from the sensor bar. How farther 
	away how thinner the line. It is also possible to switch the kind of tools used for drawing. The tools can be selected by 
	hovering over the button and using the down button. Whenever the user does not want to draw using the Wiimote anymore he 
	can press the 1 button to use the normal mouse.
	
Known issues:
- In both screen the cursor is slightly off and we had a lot of problems with the sensor bar not been found
- When you exit the application not all threads have been closed and visual studio will remain in debug mode

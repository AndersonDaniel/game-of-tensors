# game-of-tensors
Game of Tensors repo for the #PoweredByTF2 Challenge. Contains game server, game client (in Unity), mobile client UI, and the code for model training (which includes the dataset).

## Game server
Very simple Flask server which waits for a socket connection (desktop UI) and then serves endpoints for the mobile clients to push action notifications. It then forwards the actions to the desktop UI.

## Unity client
Fabulous game UI in Unity, which connects to the game server and displays the game.

## Mobile client
Simple mobile web page which runs the pre-trained model using TensorFlow.js and notifies the game server whenever a gesture has been recognised.

## Model training code
TensorFlow 2 model, including training and test set, to recognise the model.
Model's architecture:
![Gesture model architecture](https://github.com/AndersonDaniel/game-of-tensors/blob/master/model/model.png?raw=true)

Model performance, which includes for each class: the correlation between actual score and predicted score (top row) and the event timeline, with peaks for the actual and predicted confidence scores (bottom row):
![Gesture model performance](https://github.com/AndersonDaniel/game-of-tensors/blob/master/model/model_performance.png?raw=true)

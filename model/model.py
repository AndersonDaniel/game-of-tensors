import tensorflow as tf
import pandas as pd
import numpy as np
import os

FEATURES = 3
TIMESTEPS = 20

# V5
model = tf.keras.Sequential([
	tf.keras.layers.Conv1D(20, 5, activation='relu', input_shape=(TIMESTEPS, FEATURES)),
	tf.keras.layers.Dropout(0.5),
	tf.keras.layers.Conv1D(10, 10, activation='relu'),
	tf.keras.layers.Flatten(),
	tf.keras.layers.Dropout(0.5),
	tf.keras.layers.Dense(3, activation='sigmoid')
])

model.summary()

train_df = pd.read_csv('preprocessed_train.csv')
test_df = pd.read_csv('preprocessed_test.csv')

X_train = np.array([train_df[['ax', 'ay', 'az']].values[i:i + TIMESTEPS] for i in range(train_df.shape[0] - TIMESTEPS)])
y_train = np.array([train_df[['flick_score', 'shield_score', 'throw_score']].values[i + TIMESTEPS - 1] for i in range(train_df.shape[0] - TIMESTEPS)])

X_test = np.array([test_df[['ax', 'ay', 'az']].values[i:i + TIMESTEPS] for i in range(test_df.shape[0] - TIMESTEPS)])
y_test = np.array([test_df[['flick_score', 'shield_score', 'throw_score']].values[i + TIMESTEPS - 1] for i in range(test_df.shape[0] - TIMESTEPS)])

model.compile(loss='mse', optimizer='rmsprop')
model.fit(X_train, y_train, epochs=1000, batch_size=32)
model.evaluate(X_test, y_test)

evaluation_df = test_df.copy()

res = model.predict(X_test)

evaluation_df['predicted_flick_score'] = np.hstack([np.repeat([0], TIMESTEPS), res[:, 0]])
evaluation_df['predicted_shield_score'] = np.hstack([np.repeat([0], TIMESTEPS), res[:, 1]])
evaluation_df['predicted_throw_score'] = np.hstack([np.repeat([0], TIMESTEPS), res[:, 2]])

evaluation_df.to_csv('evaluation.csv', index=False)

model.save('new_model.h5')
os.system('rm -rf new_model.model')
tf.keras.experimental.export_saved_model(model, 'new_model.model')
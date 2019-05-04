import pandas as pd
import numpy as np

train = pd.read_csv('train.csv').fillna('')
test = pd.read_csv('test.csv').fillna('')

def transform(df):
	return score(df)

	return score(df)

def score(df):
	i = 0
	all_labels = sorted([x for x in df['class'].drop_duplicates().values if x])
	for label in all_labels:
		df['%s_score' % label] = 0

	while i < df.shape[0]:
		label = df.loc[i, 'class']
		if label:
			j = i + 1
			while df.loc[j, 'class'] == label:
				j += 1

			try:
				df.loc[i:j - 1, '%s_score' % label] = np.arange(1, j - i + 1) / (j - i)
			except:
				print(i)
				print(j)
				raise
			i = j + 1
		else:
			i += 1

	return df


train = transform(train)
test = transform(test)

train.to_csv('preprocessed_train.csv', index=False)
test.to_csv('preprocessed_test.csv', index=False)
import pandas as pd
import matplotlib.pyplot as plt

df = pd.read_csv('evaluation.csv')
df = df[20:]
N = df.shape[0]

plt.figure(figsize=(25, 15))

plt.subplot(231)
plt.hexbin(df['flick_score'], df['predicted_flick_score'], cmap='inferno', gridsize=25, bins='log')
plt.xlabel('flick score')
plt.ylabel('predicted flick score')
plt.title('flick accuracy')

plt.subplot(232)
plt.hexbin(df['shield_score'], df['predicted_shield_score'], cmap='inferno', gridsize=25, bins='log')
plt.xlabel('shield score')
plt.ylabel('predicted shield score')
plt.title('shield accuracy')

plt.subplot(233)
plt.hexbin(df['throw_score'], df['predicted_throw_score'], cmap='inferno', gridsize=25, bins='log')
plt.xlabel('throw score')
plt.ylabel('predicted throw score')
plt.title('throw accuracy')

plt.subplot(234)
plt.plot(range(N), df['flick_score'], alpha=0.7)
plt.plot(range(N), df['predicted_flick_score'], alpha=0.7)
plt.title('Flicks')


plt.subplot(235)
plt.plot(range(N), df['shield_score'], alpha=0.7)
plt.plot(range(N), df['predicted_shield_score'], alpha=0.7)
plt.title('Shields')


plt.subplot(236)
plt.plot(range(N), df['throw_score'], alpha=0.7)
plt.plot(range(N), df['predicted_throw_score'], alpha=0.7)
plt.title('Throws')

plt.savefig('model_performance.png')
plt.show()
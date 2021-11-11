from nltk import word_tokenize, RegexpTokenizer
import nltk
import re
import pandas as pd
from sklearn.feature_extraction.text import CountVectorizer
from sklearn.naive_bayes import MultinomialNB
import pickle
import os.path
import sys
from sklearn import svm
from sklearn import metrics
from sklearn.model_selection import cross_val_predict


def TrainingData():
    token = RegexpTokenizer(r'[a-zA-Z0-9]+') # Palavras permitidas
    vectorizer = CountVectorizer(lowercase=True, stop_words='english', ngram_range=(1, 1), tokenizer=token.tokenize) # Utilização do CountVectorizer
    sentimentos = ['0', '1', '2', '3', '4'] # Array de todos os sentimentos disponiveis.

    df = pd.read_excel('sentimentos.xlsx') # Importa base de dados com treino
    comentarios = df['Phrase'] # Separa apenas a coluna de comentários

    novos = [Preprocessing(i) for i in comentarios]

    freq = vectorizer.fit_transform(novos)

    model = MultinomialNB() # Modelo de treino utilizado
    model.fit(freq, sentimentos) # É realizado o treinamento

    pickle.dump(model, open("ModeloMultinomiaNB", 'wb')) # Aqui salvamos o modelo já treinado para não precisarmos realizar o processamento novamente.
    pickle.dump(vectorizer, open("Vectorizer", 'wb')) # Aqui salvamos o Vectorizer para não precisarmos realizar o processamento novamente.


def Preprocessing(instancia):
    instancia = re.sub(r"http\S+", "", instancia).lower().replace('.', '').replace(';', '').replace('-', '').replace(
        ':', '').replace(')', '').replace('"', '')
    stopwords = set(nltk.corpus.stopwords.words('english'))
    palavras = [i for i in instancia.split() if not i in stopwords]
    return " ".join(palavras)


comment = sys.argv[1] # Lê primeiro argumento que será o comentário.
fileExists = os.path.isfile("ModeloMultinomiaNB") #Verifica se existe um modelo de treino existente
if not fileExists:
    TrainingData() # Caso não exista, é realizado o treinamento novamente.

model = pickle.load(open("ModeloMultinomiaNB", 'rb')) #É carregado os arquivos de treino e vectorizer.
vec = pickle.load(open("Vectorizer", 'rb'))

testes = [comment]

freq_testes = vec.transform(testes)

for t, c in zip(testes, model.predict(freq_testes)):
    print(str(c))

# Documentation Technique – TheSnake
## 1. Comment jouer ?

### Principe de gameplay :

TheSnake est un jeu de type Snake classique où le joueur contrôle un serpent qui se déplace sur une grille. Le but est de manger des fruits pour grandir et marquer des points tout en évitant de se cogner contre son propre corps et les obstacles. Le serpent peut se déplacer dans quatre directions (haut, bas, gauche, droite) et la vitesse du serpent augmente progressivement au fil du jeu.

#### Il existe deux types de fruits :
- **Fruit normal** : Ajoute 1 point et fait grandir le serpent d’un segment.
- **Fruit spécial** : Donne plus de points (+5 par défaut), active un boost de score x2 pendant 5 secondes, augmente la vitesse du serpent, et fait aussi grandir le serpent.

Des obstacles apparaissent au fur et à mesure du score, rendant la progression plus difficile.

### Commandes :

- **Flèches directionnelles** : Changer la direction du serpent (haut, bas, gauche, droite).
- **P** : Pause/Reprise du jeu.
- **R (en pause)** : Recommencer la partie.
- **TAB (en pause** : Revenir au menu principal.
- **A (en pause)** : Quitter le jeu.
- **ENTER (dans menu ou écran game over)** : Commencer ou recommencer la partie.
- **ESPACE (dans le tutoriel)** : Commencer la partie.

### Comment gagner ?

Le jeu est infini et n’a pas de condition de victoire formelle. L’objectif est de réaliser le meilleur score possible en mangeant des fruits et en évitant de mourir.

### Comment perdre ?

#### Le jeu se termine lorsque :

- Le serpent se mord lui-même (collision avec son propre corps).
- Le serpent entre en collision avec un obstacle.

Dans ces cas, l’écran de Game Over s’affiche avec le score final et le meilleur score sauvegardé.

## 2. Originalité

### Spécificités de Gameplay :

- **Boost de score temporaire** : Lorsqu’un fruit spécial est mangé, le joueur bénéficie d’un doublement du score pendant une durée limitée.
- **Augmentation progressive de la vitesse** : La vitesse du serpent augmente à chaque fruit mangé, rendant le jeu de plus en plus difficile.
- **Obstacles dynamiques** : Des obstacles apparaissent automatiquement tous les 5 points, obligeant à une stratégie différente du simple "évitement du corps".
- **Gestion des directions via une queue FIFO** : Permet de gérer plusieurs entrées successives et fluidifier le changement de direction.

### Algorithme spécifique :

- **Queue FIFO pour la gestion des directions** : Dans ```GameScene.cs```, la ```Queue<Vector2> _directionQueue``` permet d’entrer plusieurs commandes de direction à l’avance, en empêchant le serpent de faire un demi-tour direct.
- **Gestion dynamique des obstacles** : Le code génère des obstacles en évitant de les placer sur le serpent ou les fruits (```AddRandomObstacle()```, ```IsPositionOnSnake()```).

### Références au code :

- **Boost de score et fruits spéciaux** : ```GameScene.cs``` – méthode ```Update()```, gestion des variables ```_scoreBoostActive```, ```_scoreBoostTimer```.
- **Queue FIFO pour les directions** : ```GameScene.cs```  – méthode ```HandleInput()``` et ```MoveSnake()```.
- **Obstacles et génération aléatoire** : ```GameScene.cs``` – méthodes ```AddRandomObstacle()```, ```SpawnFruit()```, ```SpawnSpecialFruit()```.
- **Sauvegarde du highscore** : ```HighscoreManager.cs```.
- **Gestion des scènes et transitions** : ```SceneManager.cs```.

### Expérience : 

Ce design introduit un gameplay plus riche que le snake classique en combinant la gestion de boost et d’obstacles, ainsi qu’une meilleure gestion des entrés utilisateurs. La queue pour les directions évite les mouvements brusques et améliore la jouabilité.

## 3. Votre code source :

### Structure générale :

Le jeu est organisé autour des scènes (```IScene```), gérées par un ```SceneManager``` (pile de scènes), permettant de gérer menus, tutoriels, jeu, pause, et écran de game over.

Les objets du jeu sont hérités d’une classe abstraite ```GameObject``` avec position et méthodes ```Update``` et ```Draw```.

### Les entités principales sont :

- ```snakeSegment``` : Représente un segment du serpent.
- ```fruit``` : Représente un fruit normal ou spécial.
- Obstacles représentés par des positions dans une liste.

Le gameplay est contenu dans ```GameScene```, qui gère la logique du serpent, des fruits, des obstacles et du score.

L’input est géré par un service d’entrée abstrait ```IInputService```, implémenté ici par ```KeyboardInputService```, et injecté via un ```ServiceLocator```.

### Algorithme de queue FIFO pour les directions :

#### La direction actuelle est stockée dans un ```Vector2 _direction```. Pour fluidifier les commandes, on utilise une ```Queue<Vector2> _directionQueue``` :

- À chaque frame, la direction entrée par l’utilisateur est ajoutée à la queue si elle est différente de la dernière direction en file (pour éviter les répétitions inutiles).
- Lors du mouvement du serpent ```(MoveSnake())```, on récupère la prochaine direction dans la queue.
- On empêche aussi de faire un demi-tour immédiat en vérifiant que la nouvelle direction n’est pas opposée à la direction actuelle.
- Cela permet de bufferiser plusieurs commandes et rendre le mouvement plus naturel.

### Algorithme de gestion des grilles :

- La grille est définie par ```_gridWidth = 40``` et ```_gridHeight = 30```, chaque case faisant 20 pixels.
- Le serpent, fruits, et obstacles ont des positions en coordonnées grilles (Vector2).
- Le déplacement du serpent utilise un wrapping (téléportation à l’opposé de la grille) pour gérer les limites.
- Lors de la génération des fruits et obstacles, des vérifications évitent que les objets ne se superposent (méthode ```IsPositionOnSnake()```).
- La vitesse du serpent est ajustée via un timer ```_moveTimer``` et un intervalle ```_moveInterval``` qui diminue progressivement.

### Service Locator :

Le ```ServiceLocator``` est une classe statique permettant d’enregistrer et de récupérer des services globaux par type, facilitant ainsi l’injection de dépendances sans couplage fort.

#### Services utilisés :

- ```IInputService``` (implémenté par ```KeyboardInputService```), pour abstraction de l’entrée utilisateur.

#### Role : 

- Permet d’accéder globalement aux services nécessaires au gameplay (ici principalement l’input).
- Facilite le changement d’implémentation d’input (ex : clavier, manette, IA) sans modifier la logique principale.

#### Exemple d’utilisation :
Dans ```GameScene.cs``` :
```csharp
_inputService = ServiceLocator.Get<IInputService>();
```
Puis la direction est obtenue par :
```csharp
Vector2 inputDir = _inputService.GetDirection();
```

### Informations supplémentaires :

- La gestion des scènes repose sur une pile (```Stack<IScene>```) pour permettre la superposition temporaire (pause).
- La sauvegarde du highscore est persistante via un fichier texte simple ```highscore.txt```.
- La séparation claire des responsabilités (input, rendu, logique, scènes) facilite la maintenance et l’extension du code.
- Le jeu est développé en C# avec Raylib_cs pour le rendu graphique.






# FactorioLike-MAW2.1

Factorio like est un jeu 2D, primitif fait sur monogame à la façon factorio, on peut miner des minerais, fondre, polire et crafter avec ces dernier

## Language

C#

## Termes techniques

Items : élement qui peux être transmis entre machines (example, un minerai)
Recettes : combinaisons D'items, qui permet de créer d'autre items.
Craft : Action de transformer un item en un autre.
## Conventions de nommage

### Commits

Type de commit (feat, fix, enhancement): Nom du commit

### Branches

Type de branches (feature, fix, enhancement, doc)/Nom-de-la-branche

### Assets graphique / Sprites

dans ce projet, chaques sprites, qui peut être chargé dans le jeux sont nommé de cette manière :

Pour les machines de Craft : NomDeLaMachine+Type.png, le type represent quelle partie du sprite c'est, par example, les ombres, les highlights, etc, il sont dans des images differentes, mais represente le même object

Pour les minerais : TypeDeMetal+Ore.png, le type de métal, peux être par example, du fer, du cuivre, etc... "Ore", est fixe, il represent le type de métal c'est, example, on peut avoir IronBar.png, IronPlate.png, c'est bien de pouvoir faire la différence



## Technologie

- MonoGame(3.8.1)
- MonoGameExtended
- Dotnet(7.0)
- PlantUml

### Fonctionnalitées

- 4 types machines (mineur, fondrie, polisseur, crafteur)
- Des mineraux (un au minimum)
- Transmission d'items en machines en machines
- Recettes simples (minerais fondu, minerais polis, plaque de métal)
- Placer les machines sur le terrain
- Terrain statique
- Movement du joueur (gauche, droite, bas, haut)

#### Si le temps le permets

- Génération du terrain procédurale
- Ennemis qui charge (Roaming)
- Inventaire du joueur

### Environment de travail

Visual studio community 2022


## Installation

allez sur ce site : https://visualstudio.microsoft.com/fr/vs/community/

et installez Visual studio community 2022

Après avoir l'avoire installé, allez sur ce site : https://monogame.net/articles/getting_started/1_setting_up_your_development_environment_windows.html et installez monogame

Vous êtes prêt pour participer au projet

## Build

Pour build le jeu, il suffit de cliquer sur le bouton "Play"
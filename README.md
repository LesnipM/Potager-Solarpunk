# Le Potager Solarpunk

Jeu Unity en vue de dessus : tu pilotes un drone horticole au-dessus d'un potager urbain solarpunk. Plante des graines, arrose-les pour qu'elles poussent plus vite, puis récolte les légumes au bon moment. Un légume trop mûr éclate. Fais le meilleur score avant la fin du temps imparti.

Projet de synthèse de la Partie 2 (Projet 2), réalisé individuellement.

Jouer en ligne : [lien de la page itch.io]

## Principe du jeu

- Le drone vole à hauteur constante au-dessus du terrain.
- Une graine plantée passe par trois étapes : graine, légume mûr, légume trop mûr, puis éclate.
- L'arrosage accélère la croissance des plantations situées sous le drone.
- Seul un légume mûr peut être récolté, et chaque récolte rapporte des points.
- La partie se termine quand le temps est écoulé ([90] secondes par défaut).
- Le meilleur score est conservé d'une session à l'autre (PlayerPrefs).

## Contrôles

| Action | Clavier | Manette |
| --- | --- | --- |
| Déplacement | ZQSD / WASD / flèches | Stick gauche |
| Planter | Espace | Bouton West |
| Arroser (maintenir) | E | Bouton West |
| Récolter / Ramasser | A | Bouton East |
| Pause | Échap | Start |

Tous les contrôles passent par un seul asset Input Actions (`Assets/Input`), sans script différent par périphérique.

## Installation et ouverture du projet

1. Installer Unity [version, par exemple 6000.x.x] avec le module WebGL Build Support.
2. Cloner le dépôt : `git clone [url du dépôt]`
3. Ouvrir le dossier dans Unity Hub (Add > Add project from disk).
4. Ouvrir la scène `Assets/Scenes/Menu` et lancer le Play Mode.

Les dossiers `Library`, `Temp`, `Logs` et `Builds` ne sont pas versionnés : Unity les régénère à l'ouverture.

## Structure du projet

- `Assets/Scripts` : scripts C# (un script, une responsabilité)
- `Assets/Prefabs` : plantations (variantes par légume), effets, drone
- `Assets/Scenes` : Menu, Game, End (dans cet ordre dans la build)
- `Assets/Input` : asset Input Actions
- `Assets/Animations`, `Assets/Audio`, `Assets/Models`, `Assets/Materials`, `Assets/Textures`
- `Credits.md` : crédits de tous les assets utilisés
- `Conventions.md` : norme de nommage et de code appliquée

## Réglages depuis l'Inspector

- Vitesse et limites de déplacement du drone : composant `Drone Movement`
- Durées de croissance et bonus d'arrosage : composant `Plant Growth` du Prefab `Plant_Base`
- Durée de la partie et objectif de score : composant `Game Manager` (scène Game)
- Points par récolte : composant `Score Manager`

## Scripts principaux

| Script | Rôle |
| --- | --- |
| `DroneMovement` | Déplacement du drone à hauteur constante, limité au potager |
| `DronePlanter` | Fait apparaître une graine sous le drone et lui transmet le score |
| `DroneWatering` | Arrose les plantations sous le drone, jet de particules et son |
| `PlantGrowth` | Étapes de croissance, bonus d'arrosage, éclatement |
| `PlantHarvest` | Récolte du légume mûr quand le drone est au-dessus |
| `ScoreManager` / `ScoreUI` | Score et affichage |
| `GameManager` / `TimerUI` | Timer et fin de partie |
| `PauseMenu` / `SceneLoader` | Menu pause, changement de scène |
| `SaveManager` / `ScoreboardUI` | Sauvegarde et affichage du meilleur score |

## Crédits

Voir [Credits.md](Credits.md).

## Auteur

Mathis Perrin, BUT Informatique, 2026.

# 🚀 PortoWeb - Portfolio de Gestion & Présentation

## 📌 Présentation du Projet
**PortoWeb** est une plateforme web robuste développée en **ASP.NET Core MVC**. Ce projet a été conçu pour centraliser et présenter mes réalisations techniques tout en intégrant des fonctionnalités de gestion dynamique. L'objectif principal est de démontrer une maîtrise du cycle de développement complet, de la conception de la base de données à l'interface utilisateur.

## 🛠️ Stack Technique
* **Framework :** ASP.NET Core 8.0 (Architecture MVC)
* **Langage :** C# 12
* **Base de Données :** SQL Server (Entity Framework Core)
* **Frontend :** Razor Pages, HTML5, CSS3 (Bootstrap), JavaScript
* **Outils :** Visual Studio 2022, GitHub, NuGet Package Manager

## ✨ Fonctionnalités Clés
* **Dashboard d'Administration :** Interface sécurisée pour la gestion des contenus (projets, compétences, expériences).
* **Gestion des Projets :** CRUD complet pour l'ajout, la modification et la suppression de projets avec téléchargement d'images.
* **Authentification :** Système de connexion sécurisé (ASP.NET Core Identity) pour restreindre l'accès à la partie administrative.
* **Contact & Feedback :** Formulaire de contact fonctionnel avec validation des données côté serveur.

## 🏗️ Architecture du Projet
Le projet suit les principes de séparation des préoccupations :
* **Models :** Définition des entités de données et gestion de la logique métier via EF Core.
* **Views :** Interfaces dynamiques utilisant le moteur de template Razor.
* **Controllers :** Gestion des requêtes HTTP et coordination entre les modèles et les vues.
* **Migrations :** Gestion versionnée du schéma de la base de données.

## 🚀 Installation et Lancement
Pour exécuter ce projet localement :

1.  **Cloner le repository :**
    ```bash
    git clone https://github.com/HajarSmh/PortoWeb.git
    cd PortoWeb
    ```
2.  **Configurer la base de données :**
    Modifiez la chaîne de connexion dans le fichier `appsettings.json` :
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PortoWebDB;Trusted_Connection=True;"
    }
    ```
3.  **Appliquer les migrations :**
    Dans la console du gestionnaire de packages :
    ```powershell
    Update-Database
    ```
4.  **Lancer l'application :**
    Appuyez sur `F5` dans Visual Studio ou utilisez la commande :
    ```bash
    dotnet run
    ```

---

Développé par Hajar Samouh Élève Ingénieure en 4ème année - Spécialisation IA et Data

![img](./public/Screenshot%20from%202024-12-04%2022-33-43.png)

# TemuRuang

TemuRuang is a digital platform for searching and booking event spaces, designed to simplify event planning. 

## 🔎 Background

In today's fast-paced digital era, finding venues or spaces for events such as meetings, weddings, or seminars often takes time and can be challenging to manage efficiently. Many people resort to manually searching through various scattered sources, often resulting in confusion and delays. TemuRuang emerges as a digital solution that simplifies the process of searching and booking event spaces practically and quickly. With this platform, users can easily find spaces that suit their needs through search filters based on location, capacity, price, and available facilities. This streamlines event planning, making it more efficient and effective.

## ✅ Features

- User can register and login to the system
- User can see list of workspaces available
- User can reserve or book workspace
- User can cancel workspace reservation
- User can update profile
- User can see list of articles available

## 🚀 Setup

You need docker to bootstrap TemuRuang application

1. Clone both repositories including frontend and backend

```zsh
git clone https://github.com/devanfer02/temuruang-fe.git
git clone https://github.com/devanfer02/temuruang-be.git
```

2. Navigate to backend directory and copy the `appsettings.json` file

```zsh
cd ~/temuruang-be
cp appsettings.Development.json appsettings.json
```

3. Configure the `appsettings.json` file, makesure the database host is `db` not localhost

4. After that, spin up the backend containers

```zsh
docker compose up -d
```

5. Navigate to frontend directory and configure the `.env` like `.env.example`

```zsh
cp ~/temuruang-fe
cp .env.example .env
```

6. Spin up the frontend containers

```zsh
docker compose up -d
```

7. Check [localhost:5050](http://localhost:5050) to verify if it's working or not

## 🗃️ Repositories

- [FrontEnd](https://github.com/devanfer02/temuruang-fe)
- [BackEnd](https://github.com/devanfer02/temuruang-be)

## 🛢 Database Deisgn

![img](./public/temuruang.png)

## 🛠️ Tech Stacks

<table border="1" >
  <thead>
    <tr>
      <th>App</th>
      <th>Stacks</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>Frontend</td>
      <td>
        <img src="https://skillicons.dev/icons?i=react,typescript,tailwind,bootstrap" alt="React" />
      </td>
    </tr>
    <tr>
      <td>Backend</td>
      <td>
        <img src="https://skillicons.dev/icons?i=cs,net,postgres,docker" alt="C#" />
      </td>
    </tr>
  </tbody>
</table>

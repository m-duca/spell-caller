# 🧙‍♂️ Spell Caller  
![Demonstração do sistema de conjuração por voz](./ReadmeAssets/casting_portal.gif)

**Spell Caller** é um protótipo desenvolvido na Unity com foco em experimentação de mecânicas que são ativadas a partir do reconhecimento de voz com palavras-chave.  

---

## 🎯 Objetivo do Projeto
![Demonstração do sistema de conjuração por voz](./ReadmeAssets/casting_thunder.gif)

Inspirado em jogos como **[Mage Arena](https://store.steampowered.com/app/3716600/Mage_Arena)**, o objetivo principal foi desenvolver um sistema de magias customizável onde o jogador tenha que ler o feitiço em voz alta para conjurá-lo. Além disso, o projeto é um exercício de arquitetura limpa e boas práticas de programação em Unity.

---

## 🧩 Tecnologias Utilizadas

- `Unity Engine (C#)`
- `Windows Speech Recognition API` – Sistema de **reconhecimento de voz nativo**, utilizando `KeywordRecognizer` para acionar magias com comandos falados.  
- `ScriptableObjects`– Armazenamento modular das magias (nome de conjuração, dano, intervalo, efeitos, informações na HUD).  
- `FMOD` – Sistema de áudio dinâmico (som ambiente, efeitos sonoros com variação lógica e espacial 3D).  
- `DOTween` – Aplicação de juiceness e feedbacks visuais (headbob, suavização física, animações na UI).  
- `Input System (New)` – Suporte a teclado e controle.  

---

## 📊 Status Atual

🔹 **Fase:** Base finalizada 

🔹 **Foco atual:** Demonstrar o resultado atingido com as conjunrações de magias

🔹 **Próximos passos:**  
- Build pública
- Implementação de novos feitiços e inimigos  

---

## 📦 Créditos de Assets

### Props
- **[Low Poly Casual Magic Book](https://assetstore.unity.com/packages/3d/props/low-poly-casual-magic-book-289381)** – Revereel Studio
- **[Low_Poly Nature](https://assetstore.unity.com/packages/3d/environments/low-poly-nature-260306)** – Oode Studios
- **[Lowpoly training dummy](https://assetstore.unity.com/packages/3d/props/lowpoly-training-dummy-202311)** – iltaen
- **[Magic Effects FREE](https://assetstore.unity.com/packages/p/magic-effects-free-247933)** – Hovl Studio
- **[Mountain Terrain, Rocks and Tree](https://assetstore.unity.com/packages/3d/environments/landscapes/mountain-terrain-rocks-and-tree-97905)** – Jermesa Studio
- **[Particle Pack](https://assetstore.unity.com/packages/vfx/particles/particle-pack-127325)** – Unity Technologies
- **[Rocks and Terrains Pack - Low Poly](https://assetstore.unity.com/packages/3d/environments/rocks-and-terrains-pack-low-poly-281733)** – HQP Studios
- **[Zap VFX - URP](https://assetstore.unity.com/packages/vfx/particles/spells/zap-vfx-urp-303479)** – Vefects

### Sons
- **[Floating In The Midnight Breeze](https://freesound.org/people/FoolBoyMedia/sounds/332323)** – FoolBoyMedia
- **[AMBForst_Autumn.A Quiet Forest.Wind In The Pines And Birches.Byrds 3_EM](https://freesound.org/people/newlocknew/sounds/757872)** – newlocknew
- **[Fireball Cast 1](https://freesound.org/people/LiamG_SFX/sounds/334234)** – LiamG_SFX
- **[Icy Magic Cast](https://freesound.org/people/DustyWind/sounds/691005)** – DustyWind
- **[Freeze Cast](https://freesound.org/people/JustInvoke/sounds/446144)** – JustInvoke
- **[Magic - Spell casting 02](https://freesound.org/people/Vrymaa/sounds/770233)** – Vrymaa
- **[Knife](https://freesound.org/people/Zenleser/sounds/533573)** – Zenleser
- **[OneThunder.wav](https://freesound.org/people/marcosdegodoy/sounds/372880)** – marcosdegodoy
- **[Electric Shock 2 Hit](https://freesound.org/people/The-Sacha-Rush/sounds/657803)** – The-Sacha-Rush
- **[Mouth_Lightening.wav](https://freesound.org/people/Deganoth/sounds/165908)** – Deganoth
- **[01903 air swoosh.wav](https://freesound.org/people/Robinhood76/sounds/101386)** – Robinhood76
- **[PAPRHndl-Samsung Galaxy Smartphone, MCU_Newspaper, Page Flip_Nicholas Judy_TDC](https://freesound.org/people/designerschoice/sounds/810142)** – designerschoice
- **[Bike Drop Front Tire 1](https://freesound.org/people/Geoff-Bremner-Audio/sounds/730973)** – Geoff-Bremner-Audio
- **[Bike Drop Front Tire 2](https://freesound.org/people/Geoff-Bremner-Audio/sounds/730974)** – Geoff-Bremner-Audio
- **[hit-wood03.wav](https://freesound.org/people/JanKoehl/sounds/85584)** – JanKoehl

---

## 👁️ Observação Final

Este projeto **não é comercial** e tem como finalidade **estudos e aperfeiçoamento de conhecimentos e boas práticas**.  

---

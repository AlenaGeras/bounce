# Dokumentace projektu

## Popis zadání práce
Cílem této práce je vytvoření jednoduché 2D arkádové hry v prostředí Unity. Hráč ovládá kouli, která se pohybuje herním světem, sbírá prsteny a bonusy, vyhýbá se překážkám a interaguje s různými objekty, jako jsou například vodní plochy či nepřátelské elementy. Hra obsahuje systém úrovní, správu životů a dynamické řízení kamery.

## Velmi stručný popis řešení problému
Hra je implementována v jazyce C# ve frameworku Unity. Načítání úrovní je realizováno pomocí textových souborů, ve kterých jednotlivé znaky reprezentují specifické herní objekty (např. `#` pro překážky, `R` pro prsteny, `W` pro vodu apod.). Pohyb a fyzikální interakce koule jsou zajištěny pomocí vestavěného physics enginu Unity, přičemž dynamické sledování objektu pomocí kamery je realizováno pomocí algoritmu SmoothDamp. I když existují i jiné přístupy, jako je optimalizovaná detekce kolizí nebo alternativní metody parsování úrovní, zvolená implementace se vyznačuje jednoduchostí a přehledností.

## Autor
**Gerasymovych Alona**

### Akademický rok a studijní zaměření
- **Akademický rok:** 2024/2025  
- **Studijní zaměření:** Aplikovaná informatika


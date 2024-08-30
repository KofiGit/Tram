# Skoda

## Funkce

Aplikace se obsluhuje pomocí REST callu.
<br>
Příprava dat probíhá běheme callu **/api/job/setInitData**
<br>
Získání dat, tedy informací o tramvajích lze pomocí callu **/api/job/getTramInfo**
<br>
Přiřazení jobu první tramvaji, která nemá job se děje po zavolání callu **/api/job/assingTramJo**
<br>

## Kod

Pro data je využitá Distributed cache jako zdroj.
<br>
Pro unikátnost a rychlost jsem využil **SortedSet < T >**, který už v základu funguje v rámci O(logn)
<br>
Chybí ochrana lockování pro cache.

<script setup>
import { RouterView } from 'vue-router'
</script>

<template> 
  <div class="wrapper">
    <RouterView/>
  </div>
</template>
<!-- Стили -->
<style>
html, body {
  margin: 0;
  padding: 0;
  width: 100%;
  height: 100%;
  overflow: auto
}
#app {
  margin: 0;
  padding: 0;
  width: 100%;
  height: 100%;
}
/* Стиль скроллбара */
/* Chrome, Edge, Safari */
::-webkit-scrollbar {
    width: 8px;
    height: 8px;
}

/* Дорожка */
::-webkit-scrollbar-track {
    background: #111111;
    border-radius: 2px;
}
/* Firefox */
.app-container, * {
  scrollbar-width: thin;
  scrollbar-color: #545353 #111111;  /* цвет бегунка и трека */
}

/* Бегунок */
::-webkit-scrollbar-thumb {
    background: #545353;
    border-radius: 2px;
}

/* Бегунок при наведении курсора */
::-webkit-scrollbar-thumb:hover {
    background: #6BD2FF80;
}
</style>
<style scoped>
header {
  line-height: 1.5;
  max-height: 100vh;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

nav {
  width: 100%;
  font-size: 12px;
  text-align: center;
  margin-top: 2rem;
}

nav a.router-link-exact-active {
  color: var(--color-text);
}

nav a.router-link-exact-active:hover {
  background-color: transparent;
}

nav a {
  display: inline-block;
  padding: 0 1rem;
  border-left: 1px solid var(--color-border);
}

nav a:first-of-type {
  border: 0;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }

  nav {
    text-align: left;
    margin-left: -1rem;
    font-size: 1rem;

    padding: 1rem 0;
    margin-top: 1rem;
  }
}
</style>

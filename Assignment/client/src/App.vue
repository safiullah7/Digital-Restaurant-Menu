<template>
  <div id="app">
    <NavBar />
    <Categories />
    <Dishes />
  </div>
</template>

<script>
import Dishes from './components/Dishes/Dishes';
import NavBar from './components/NavBar/NavBar.vue';
import Categories from './components/Categories/Categories';

export default {
  name: 'App',
  components: {
    Dishes,
    NavBar,
    Categories
  }
}
</script>

<style>

</style>

<template>
  <div id="app">
    <NavBar />
    <Dishes />
  </div>
</template>

<script>
import Dishes from './components/Dishes/Dishes.vue';
import NavBar from './components/NavBar/NavBar.vue';

export default {
  name: 'App',
  components: {
    Dishes,
    NavBar
  }
}
</script>

<style>

</style>

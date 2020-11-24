<template>
  <div :style="myStyle">
    <div v-for="dish in allDishes" :key="dish.id">
      <Dish :dish="dish" />
    </div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';
import Dish from './Dish';
import Vue from 'vue';

Vue.component('Dish', Dish);
export default {
    name: "Dishes",
     data: function () {
        return {
            dish: {
              id: "",
              name: "",
              price: 0,
              category: "",
              availability: "",
              active: false,
              preparationTime: 0,
              createdAt: ""
            },
            myStyle: {
                backgroundColor:"#16a085"
            }
        }
    },
    methods: {
      ...mapActions(['fetchDishes'])
    },
    computed: mapGetters(['allDishes']),
    created() {
      this.fetchDishes();
    },
    components: {
      Dish
    }
}
</script>

<style>

</style>
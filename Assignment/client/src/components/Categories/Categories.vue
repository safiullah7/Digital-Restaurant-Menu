<template>
  <div class="container">
      <!-- <b-carousel-list :data="allCategories" :items-to-show="5">
        <template slot="item" slot-scope="list"> -->
        <b-card-group deck>
            <div v-for="list in allCategories" :key="list.id">
                <Category :category="list" />
            </div>
        </b-card-group>
        <!-- </template>
    </b-carousel-list> -->
  </div>
</template>

<script>
import Category from './Category';
import { mapGetters, mapActions } from 'vuex';

export default {
    name: "Categories",
    components: {
        Category
    },
    methods: {
      ...mapActions(['fetchCategories'])
    },
    computed: mapGetters(['allCategories']),
    created() {
      this.fetchCategories();
    }
}
</script>

<style>

</style>
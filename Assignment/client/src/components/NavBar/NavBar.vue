

<template>
    <div class="container">
        <b-navbar>
            <template slot="brand">
                <b-navbar-item>
                    <img
                        src="https://www.graphicsprings.com/filestorage/stencils/a10a9c67fd2f5a3aa7a222d9d1d86784.png?width=500&height=500"
                        alt=".NET and Vue Restaurant Menu app"
                    >
                </b-navbar-item>
            </template>
            <template slot="start">
                <b-navbar-item href="#">
                    Dishes
                </b-navbar-item>
                <b-navbar-item @click="setListMode(!listMode)">
                    Add Dish
                </b-navbar-item>
            </template>
        </b-navbar>
    </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';

export default {
    name: "NavBar",
    methods: {
      ...mapActions(['setListMode'])
    },
    computed: mapGetters(['listMode'])
}
</script>

<style>

</style>
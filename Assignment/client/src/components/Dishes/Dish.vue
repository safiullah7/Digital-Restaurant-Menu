<template>
    <div>
        <b-card style="width: 500px; margin-bottom:10px; margin-top: 10px;" :title="dish.name" align="center" sub-title="Card subtitle">
            <b-card-text>
                {{dish.description}}
            </b-card-text>

            <b-card-text>Price: {{dish.price}}</b-card-text>
            <b-card-text>Category: {{dish.category}}</b-card-text>
            <b-card-text>Availability: {{dish.availability}}</b-card-text>
            <b-card-text>Active: {{dish.active}}</b-card-text>
            <b-card-text>Created at: {{dish.createdAt}}</b-card-text>

            <div @click="deleteDish(dish.id)" class="buttons">
                    <b-button type="is-danger" icon-left="delete" expanded>Delete</b-button>
            </div>
        </b-card>
    </div>
</template>

<script>
import { mapActions } from 'vuex';

export default {
    props: ['dish'],
    name: "Dish",
    data(){
        return{
            myStyle:{
                border: 'border: 3px solid red'
            }
        }
    },
    methods: {
        ...mapActions(['deleteDish'])
    }
}
</script>

<style>
    
</style>
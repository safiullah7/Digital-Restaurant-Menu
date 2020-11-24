<template>
  <div class="card" :style="myStyle">
        <div class="card-content">
            <div class="media">
                <div class="media-content">
                    <p class="title is-4">{{dish.name}}</p>
                    <p class="subtitle is-6">@johnsmith</p>
                </div>
            </div>
            <div class="content">
                {{dish.description}}
                <br>
                <small>11:09 PM - 1 Jan 2016</small>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    props: ['dish'],
    name: "Dish",
    data(){
        return{
            myStyle:{
                border: 'border: 3px solid red'
            }
        }
    }
}
</script>

<style>
    
</style>
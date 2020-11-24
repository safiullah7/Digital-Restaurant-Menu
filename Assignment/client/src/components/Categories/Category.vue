<template>
  <div>
        <b-card style="margin-bottom:10px; margin-top: 10px;" 
            :title="category.name" align="center">
            
            <b-card-text>Created at: {{category.createdAt}}</b-card-text>
        </b-card>
    </div>
</template>

<script>
export default {
    name: "Category",
    props: ['category']
}
</script>

<style>

</style>
<template>
  <section style="width: 50%; margin-left: 25%">
    <b-form @submit="onSubmit" @reset="onReset" v-if="show">
      <b-form-group id="input-group-2" label="Name:" label-for="input-2">
        <b-form-input
          id="input-2"
          v-model="form.name"
          required
          placeholder="Enter name"
        ></b-form-input>
      </b-form-group>
      <b-form-group id="input-group-1" label="Name:" label-for="input-1">
        <b-form-textarea
          id="input-1"
          v-model="form.description"
          rows="2"
          max-rows="6"
          required
          placeholder="Enter Description"
        ></b-form-textarea>
      </b-form-group>

      <b-form-group id="input-group-price" label="Price:" label-for="input-price">
        <b-input-group prepend="$" append=".00">
            <b-form-input id="input-price"
                v-model="form.price"
                required
                placeholder="Enter Price"
            ></b-form-input>
        </b-input-group>
      </b-form-group>

      <b-form-group id="input-group-3" label="Category:" label-for="input-3">
        <b-form-select
          id="input-3"
          v-model="form.category"
          :options="categories"
          required
        ></b-form-select>
      </b-form-group>

      <b-form-group id="input-group-4" label="Availability:" label-for="input-4">
        <b-form-select
          id="input-4"
          v-model="form.availability"
          :options="availabilities"
          required
        ></b-form-select>
      </b-form-group>

      <b-form-group id="input-group-prepTime" label="Preparation Time:" label-for="input-prepTime">
        <b-input-group append="minutes">
            <b-form-input id="input-prepTime"
                v-model="form.preparationTime"
                required
                placeholder="Enter Preparation Time"
            ></b-form-input>
        </b-input-group>
      </b-form-group>

      <b-button type="submit" @click="submitForm" variant="primary">Submit</b-button>
      <b-button type="reset" variant="danger">Reset</b-button>
    </b-form>
    
  </section>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';

export default {
    name: "AddDish",
    data() {
        return {
            form: {
                name: '',
                description: '',
                price: null,
                category: null,
                availability: null,
                active: true,
                preparationTime: null
            },
            categories: [{ text: 'Select One', value: null }, 'Chinese', 'Italian', 'Thai', 'Mexican'],
            availabilities: [{ text: 'Select One', value: null }, 'Breakfast', 'Lunch', 'Dinner'],
            show: true
        }
    },
    methods: {
        ...mapActions(['setListMode', 'addDish']),
        
        submitForm() {
            // alert(JSON.stringify(this.form));
            this.addDish(this.form);
            this.setListMode(!this.listMode);
        },
        onSubmit(evt) {
            evt.preventDefault()
            alert(JSON.stringify(this.form))
        },
        onReset(evt) {
            evt.preventDefault()
            // Reset our form values
            this.form.name = '',
            this.form.description = '',
            this.form.price = null,
            this.form.category = null,
            this.form.availability = null,
            this.form.active = false,
            this.form.preparationTime = null
            // Trick to reset/clear native browser form validation state
            this.show = false
            this.$nextTick(() => {
            this.show = true
            })
        }
    },
    computed: mapGetters(['listMode'])
}
</script>

<style>

</style>
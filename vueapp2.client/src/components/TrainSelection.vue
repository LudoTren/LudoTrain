<script setup>
    import { ref } from 'vue'
    
    const selected = ref('A')
    const selected2 = ref('A')
const options = ref([
  { text: 'One', value: 'A' },
  { text: 'Two', value: 'B' },
  { text: 'Three', value: 'C' }
])
</script>
<template>
    <b-form-datepicker id="example-datepicker" v-model="value" class="mb-2"></b-form-datepicker>
    <select v-model="selected">
        <option v-for="option in post" :value="option.idStazione">
            {{ option.nomeStazione }}
        </option>
    </select>

    <div>Selected: {{ selected }}</div>

    <select v-model="selected2">
        <option v-for="option in post" :value="option.idStazione">
            {{ option.nomeStazione }}
        </option>
    </select>

    <div>Selected: {{ selected2 }}</div>
</template>

<script>
    import { defineComponent } from 'vue';

    export default defineComponent({
        components: {
        },
        directives: {
        },
        filters: {
        },
        props: {
        },
        data() {
            return {

                loading: false,
                post: null
            };
        },
        computed: {
        },
        watch: {
        },
        beforeCreate() {
        },
        created() {
            this.fetchData();
        },
        beforeMount() {
        },
        mounted() {
        },
        updated() {
        },
        activated() {
        },
        deactivated() {
        },
        beforeDestroy() {
        },
        destroyed() {
        },
        methods: {
            fetchData() {
                this.post = null;
                this.loading = true;

                fetch('autoCompleteStation')
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Network response was not ok');
                        }
                        return response.json();
                    })
                    .then(json => {
                        this.post = json;
                        this.loading = false;
                    })
                    .catch(error => {
                        console.error('There was a problem with the fetch operation:', error);
                        // Gestione degli errori
                        this.loading = false;
                    });
            }
        },
    });
</script>

<style>
</style>
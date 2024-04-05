<script setup>
    const props = defineProps(['msg']);
</script>
<template>
    
    <div>
        <div v-if="post" class="content">
            <table>
                <thead>
                    <tr>
                        <th>Stazione</th>
                        <th>Ritardo</th>
                        <th>Arrivo</th>
                        <th>Partenza</th>
                        <th>Arrivo Reale</th>
                        <th>Partenza Reale</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="forecast in post.Fermate" :key="forecast.id">
                        <td>{{forecast.Stazione }}</td>
                        <td>{{forecast.Ritardo }}</td>
                        <td>{{forecast.Arrivo_Teorico }}</td>
                        <td>{{forecast.Partenza_Teorica }}</td>
                        <td>{{forecast.ArrivoReale }}</td>
                        <td>{{forecast.PartenzaReale }}</td>

                    </tr>
                </tbody>
            </table>
            <p> Ultimo Rilevamento: {{post.StazioneUltimoRilevamento}} {{post.OraUltimoRilevamento}}  {{post.RitardoUltimoRilevamento}}</p>
        </div>
    </div>
    
</template>
<script>
    import { defineComponent } from 'vue';
    export default defineComponent({
        data() {
            return {
                
                loading: false,
                post: null
            };
            
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.fetchData(this.msg);
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            fetchData(codice) {
                this.post = null;
                this.loading = true;

                fetch('train?codice='+codice)
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

<style scoped>
th {
    font-weight: bold;
}
tr:nth-child(even) {
    background: #000000;
}

tr:nth-child(odd) {
    background: #000000;
}

th, td {
    padding-left: .5rem;
    padding-right: .5rem;
}
.content{
    text-align: left;
}

table {
    margin-left: auto;
    margin-right: auto;
}
</style>
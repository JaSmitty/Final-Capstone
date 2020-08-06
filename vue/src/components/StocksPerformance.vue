<template>
  <div>

  </div>
</template>

<script>
import stocksService from '@/services/StockService'
export default {
    data() {
        return {
            stocksData: []
        }
    },
    methods: {
        
    },
    created() {
        stocksService.getStocks().then(response => {
            if (response.status === 200) {
                this.stocksData = response.data
            }
        })
    }
}
</script>

<style>

</style>
<template>
  <div>
      <h1>Current investments</h1>
      <ul>
          <li v-for="investment in investments" :key="investment.id">
              <p>{{investment.company_ticker}}</p>
          </li>
      </ul>
  </div>
</template>

<script>
export default {
    data() {
        return {
            investments: [
                {id: 1, users_id: 1, company_ticker: 'AAPL', game_id: 1, initial_shares: 2, current_shares: 2, price_per_share: 90}
            ]
        }
    }
}
</script>

<style>

</style>
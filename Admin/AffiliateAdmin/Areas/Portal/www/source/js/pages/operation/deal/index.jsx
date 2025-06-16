import React from 'react';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../../modules/store';
import ClaimPrincipal from '../../../modules/claim';

const App = () => {
    const { dealTypes, targetCountries, products } = window.initialState;
    const claimsPrincipal = ClaimPrincipal.currentClaimPrincipal;
    const store = configureStore({ claimsPrincipal, dealTypeList: dealTypes, targetCountryList: targetCountries, productList: products }, reducer);

    console.log("initial store!");

    return (
        <Provider store={store}>
            <AppContainer />
        </Provider>
    );
}

export default App;
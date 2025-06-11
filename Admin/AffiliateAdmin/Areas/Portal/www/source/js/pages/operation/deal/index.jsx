import React from 'react';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../../modules/store';

const { dealTypes, targetCountries, products } = window.initialState;

const store = configureStore({ dealTypeList: dealTypes, targetCountryList: targetCountries, productList: products }, reducer);

const App = () => {

    return (
        <Provider store={store}>
            <AppContainer />
        </Provider>
    );
}

export default App;
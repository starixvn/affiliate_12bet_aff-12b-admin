import React from 'react';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../../modules/store';
import { convertArrayToObjectProps } from '../../../modules/utils';

const { dealTypes, targetCountries, products } = window.initialState;

const targetCountryList = convertArrayToObjectProps(targetCountries, 'TargetCountryCode', 'TargetCountryName');

const store = configureStore({ dealTypeList: dealTypes, targetCountryList, productList: products }, reducer);

const App = () => {

    return (
        <Provider store={store}>
            <AppContainer />
        </Provider>
    );
}

export default App;
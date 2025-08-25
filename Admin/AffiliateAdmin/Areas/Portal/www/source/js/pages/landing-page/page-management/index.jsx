import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';

const App = () => {
    const { targetCountryList, languageList, dealList, statusList, landingPageUrl } = window.initialState;

    const store = configureStore({
        targetCountryList,
        languageList,
        dealList,
        statusList,
        landingPageUrl
    }, reducer);

    return (
        <Provider store={store}>
            <AppContainer />
        </Provider>
    );
};

export default App;
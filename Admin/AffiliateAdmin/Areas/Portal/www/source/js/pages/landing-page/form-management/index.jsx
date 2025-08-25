import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';

const App = () => {
    const { targetCountryList, languageList, bannerRootUrl } = window.initialState;

    const store = configureStore({ targetCountryList, languageList, bannerRootUrl }, reducer);

    return (
        <Provider store={store}>
            <AppContainer />
        </ Provider>
    );
}

export default React.memo(App);
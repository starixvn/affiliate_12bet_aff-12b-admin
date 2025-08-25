import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';

const App = () => {
    const { moduleList, targetCountryList, languageList, maxFileSize, maximumFiles, allowedExtensions, bannerRootUrl } = window.initialState;

    const store = configureStore({
        moduleList,
        targetCountryList,
        languageList,
        maxFileSize,
        maximumFiles,
        allowedExtensions,
        bannerRootUrl
    }, reducer);

    return (
        <Provider store={store}>
            <AppContainer />
        </Provider>
    );
};

export default App;
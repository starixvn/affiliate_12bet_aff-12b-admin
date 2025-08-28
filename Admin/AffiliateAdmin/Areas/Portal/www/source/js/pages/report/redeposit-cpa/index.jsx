import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import { configureStore } from '../../../modules/store';
import reducers from './reducers';

const store = configureStore({
    ...window.initialState,
}, reducers);

export const App = () => {
    return <Provider store={store}>
        <AppContainer />
    </Provider>;
};

export default App;
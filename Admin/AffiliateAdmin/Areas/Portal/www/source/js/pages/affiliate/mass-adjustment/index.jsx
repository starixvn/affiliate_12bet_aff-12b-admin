import React from 'react';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../../modules/store';

const store = configureStore({}, reducer);

const App = (props) => {

    return (
        <Provider store={store}>
            <AppContainer />
        </Provider>
    );
}

export default App;

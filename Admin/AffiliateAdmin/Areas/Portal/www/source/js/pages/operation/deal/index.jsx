import React from 'react';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../../modules/store';
import { convertArrayToObjectProps } from '../../../modules/utils';

const { dealTypes } = window.initialState;
const dealTypeList = convertArrayToObjectProps(dealTypes, 'GroupTypeId', 'GroupTypeName');

const store = configureStore({ dealTypeList }, reducer);

const App = () => {

    return (
        <Provider store={store}>
            <AppContainer />
        </Provider>
    );
}

export default App;
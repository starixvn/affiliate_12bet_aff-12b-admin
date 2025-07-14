import React from "react";
import { Provider } from "react-redux";

import reducer from './reducers';
import { configureStore } from '../../../modules/store';
import { convertListToMutilpleSelected } from '../../../modules/utils';
import AppContainer from "./components/AppContainer";

const App = () => {
    const store = configureStore({
        ...window.initialState,
    }, reducer);


    return (
        <Provider store={store}>
            <AppContainer />
        </Provider>
    );
}

export default App;
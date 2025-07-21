import React from "react";
import { Provider } from "react-redux";
import { configureStore } from '../../../modules/store';
import reducer from './reducers';
import AppContainer from './components/AppContainer'

export default function App() {
    const store = configureStore({}, reducer)
    console.log('store', store.getState())

    return (
        <Provider store={store}>
            <AppContainer/>
        </Provider>
    );
}
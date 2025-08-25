import React from 'react';
import { Provider } from 'react-redux';

import AssetUploaderContainer from './components/AssetUploaderContainer';
import reducer from './reducers';
import { configureStore } from '../../../modules/store';

const App = () => {
    const store = configureStore({ }, reducer);

    return (
        <Provider store={store}>
            <AssetUploaderContainer />
        </Provider>
    );
}

export default React.memo(App);
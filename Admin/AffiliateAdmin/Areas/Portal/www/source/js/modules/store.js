import { createStore, applyMiddleware, compose } from "redux";
import thunkMiddleware from "redux-thunk";

export const configureStore = (initialState, reducer) => {
    const enhancer = compose(
        applyMiddleware(
            thunkMiddleware // lets us dispatch() functions
        )
    );

    return createStore(reducer, initialState, enhancer);
}
import React from 'react';
import { Item } from '../resetAllProps';
import { shallow } from 'enzyme';

describe('admin role - core - components - resetAllProps', () => {
    let props, result;
    beforeEach(() => {
        const Test = () => (<h1>Test</h1>)
        props = { PageComponent: Test, name: test, resetReducer: jest.fn() }
        result = shallow(<Item {...props} />)
    })

    it('render correctly', () => {
        expect(result).toMatchSnapshot();
    });

    it('componentWillUnmount - should call resetAllProps', () => {
        result.instance().componentWillUnmount()

        expect(props.resetReducer).toHaveBeenCalled()
    });
});
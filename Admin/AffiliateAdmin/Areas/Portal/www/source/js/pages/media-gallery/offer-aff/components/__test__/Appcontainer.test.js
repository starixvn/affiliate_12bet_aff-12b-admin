import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';

describe('offer aff - components - AppContainer', () => {
    const defaultProps = {
        param: {},
        isShowLoader: true,
        gridData: [],
        checkedIds: {},
        isModifiable: true
    };

    it('render correctly', () => {
        const result = shallow(<AppContainer {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with has not permission', () => {
        const props = Object.assign({}, defaultProps, { isModifiable: false });
        const result = shallow(<AppContainer {...props} />);

        expect(result).toMatchSnapshot();
    });
});
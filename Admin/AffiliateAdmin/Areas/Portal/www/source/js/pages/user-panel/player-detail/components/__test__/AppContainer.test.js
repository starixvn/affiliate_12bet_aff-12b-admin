import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';

describe('player detail - components - AppContainer', () => {
    const defaultProps = {
        sortedParam: { sortById: 5, isSortAsc: false },
        hasTurnOverPermission: true,
        setSortedParam: jest.fn()
    }

    it('render correctly', () => {
        const result = shallow(<AppContainer {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly when sortedParam is null', () => {
        const props = Object.assign({}, defaultProps, { sortedParam: null, hasTurnOverPermission: false });
        const result = shallow(<AppContainer {...props} />);

        expect(result).toMatchSnapshot();
    });
});
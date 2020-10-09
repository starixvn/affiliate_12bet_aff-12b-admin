import React from 'react';
import { shallow } from 'enzyme';
import SortingCell from '../SortingCell';

describe('players detail - components - SortingCell', () => {
    const defaultProps = {
        isSortAsc: false,
        sortingColumn: 5,
        columnValue: 5,
        title: 'Turnover',
        handleClickHeader: jest.fn()
    }

    it('render correctly with sort ASC', () => {
        const props = Object.assign({}, defaultProps, { isSortAsc: true });

        const result = shallow(<SortingCell {...props} />);

    expect(result).toMatchSnapshot();
    });

    it('render correctly with sort DESC', () => {
        const result = shallow(<SortingCell {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('handleClickHeader should be called when click button', () => {
        const result = shallow(<SortingCell {...defaultProps} />);

        result.find('Button').simulate('click');

        expect(defaultProps.handleClickHeader).toHaveBeenCalledWith(5);
    });
});
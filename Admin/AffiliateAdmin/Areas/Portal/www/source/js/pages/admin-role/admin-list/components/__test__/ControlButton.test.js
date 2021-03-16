import React from 'react';
import { ControlButtons } from '../ControlButtons';
import { shallow } from 'enzyme';

import ButtonDeleteItems from '../../../../common/components/ButtonDeleteItems';

describe('admin list - components - ControlButtons ', () => {
    const gridData = [{ Id: 123 }];

    const param = {
        UserName: null
    };

    const defaultProps = {
        checkedUsers: { 123: false },
        param,
        gridData
    };

    it('render correctly', () => {
        const result = shallow(<ControlButtons {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with gridData is null', () => {
        const props = Object.assign({}, defaultProps, { gridData: null })
        const result = shallow(<ControlButtons {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with gridData is empty', () => {
        const props = Object.assign({}, defaultProps, { gridData: [] })
        const result = shallow(<ControlButtons {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('fetchData should be called when click button Delete', () => {
        const fetchData = jest.fn();
        const props = Object.assign({}, defaultProps, { fetchData })
        const result = shallow(<ControlButtons {...props} />);

        result.find(ButtonDeleteItems).props().fetchData();

        expect(props.fetchData).toHaveBeenCalledWith(param);
    });

    it('_handleClickButton should call redirectToCreateNew', () => {
        const props = Object.assign({}, defaultProps, { redirectToCreateNew: jest.fn() })
        const result = shallow(<ControlButtons {...props} />);

        result.find('Button').simulate('click');

        expect(props.redirectToCreateNew).toHaveBeenCalled();
    });
})
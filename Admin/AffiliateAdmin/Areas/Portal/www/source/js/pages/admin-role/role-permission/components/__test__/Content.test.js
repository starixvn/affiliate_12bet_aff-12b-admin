import React from 'react';
import { Content } from '../Content';
import { shallow } from 'enzyme';
import MyLink from '../../../../common/components/MyLink';

describe('role permission - components - Content', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    const defaultProps = {
        isGeneral: true,
        getRoleList: jest.fn(),
        getMarketList: jest.fn(),
    }

    it('render correctly', () => {
        const result = shallow(<Content {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with not isGeneral', () => {
        const props = Object.assign({}, defaultProps, { isGeneral: false });
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('_handleChangeTab - should call setIsGeneral with true', () => {
        const props = Object.assign({}, defaultProps, { setIsGeneral: jest.fn() });
        const result = shallow(<Content {...props} />);

        result.find(MyLink).at(0).props().onClick();

        expect(props.setIsGeneral).toHaveBeenCalledWith(true);
    });

    it('_handleChangeTab - should call setIsGeneral with false', () => {
        const props = Object.assign({}, defaultProps, { setIsGeneral: jest.fn() });
        const result = shallow(<Content {...props} />);

        result.find(MyLink).at(1).props().onClick();

        expect(props.setIsGeneral).toHaveBeenCalledWith(false);
    });
});
import React from 'react';
import UserPanelSubMenu from '../UserPanelSubMenu';
import { shallow } from 'enzyme';
import { NavLink } from 'reactstrap';

describe('common - components - UserPanelSubMenu', () => {
    it('render correctly', () => {
        const subMenuList = [
            { isActive: true, handleClick: jest.fn(), title: 'Statement History' },
            { isActive: false, handleClick: jest.fn(), title: 'Edit' }
        ];

        const result = shallow(<UserPanelSubMenu subMenuList={subMenuList} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with subMenuList is null', () => {
        const result = shallow(<UserPanelSubMenu />);

        expect(result).toMatchSnapshot();
    });

    it('handleClick - should call handleClick when click menu', () => {
        const subMenuList = [
            { isActive: true, handleClick: jest.fn(), title: 'Statement History' },
            { isActive: false, handleClick: jest.fn(), title: 'Edit' }
        ];

        const result = shallow(<UserPanelSubMenu subMenuList={subMenuList} />);

        result.find(NavLink).at(1).props().onClick({ preventDefault: jest.fn() })

        expect(subMenuList[1].handleClick).toHaveBeenCalled();
    });
});
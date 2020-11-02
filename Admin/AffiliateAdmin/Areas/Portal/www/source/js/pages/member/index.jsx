import React, { Suspense } from 'react';
import ReactDOM from 'react-dom';
import ClaimPrincipal from '../../modules/claim';
import MenuList from '../common/components/MenuList';
import { menus } from './modules/menus';
import { BrowserRouter as Router, Route, Switch, StaticRouter } from 'react-router-dom'

export default class App extends React.PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            activeIndex: 0
        }
    }

    componentDidMount = () => {
        var pathName = window.location.pathname;
        var renderMenus= this._getRenderMenuItems();
        var index = renderMenus.findIndex((menu) => menu.href.toLowerCase() === pathName.toLowerCase());
        this.setState({activeIndex: index});
    }

    _getRenderMenuItems = () => {
        const claims = new ClaimPrincipal(window.globalVariables.claims);
        const renderMenuLst = menus.filter((menu) => claims.hasClaim(menu.renderClaim));
        return renderMenuLst;
    }

    _handleClickMenu = (index) => {
        this.setState({ activeIndex: index });
    }

    render() {
        const { activeIndex } = this.state;
        var renderMenuLst = this._getRenderMenuItems();
        const imgSrc = 'Areas/Portal/www/dist/imgs/i_member.png';
        return (
            <Router>
                <MenuList img={imgSrc}
                    activeIndex={activeIndex}
                    menus={renderMenuLst.map((menu) => { return { label: menu.label, href: menu.href } })}
                    handleClick={this._handleClickMenu} />
                <Suspense fallback={null}>
                    <Switch>
                        {renderMenuLst.map((menu) => <Route path={menu.href} component={menu.component} />)}
                    </Switch>
                </Suspense>
            </Router>
        )
    }
}

ReactDOM.render(<App />, document.getElementById("root"));
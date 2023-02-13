import React, { Suspense } from 'react';
import ReactDOM from 'react-dom';
import ClaimPrincipal from '../../modules/claim';
import MenuList from '../common/components/MenuList';
import { menus } from './libs/menus.jsx';
import { BrowserRouter as Router, Route, Switch, Redirect } from 'react-router-dom'

export default class App extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    _getRenderMenuItems = () => {
        const claims = ClaimPrincipal.currentClaimPrincipal;
        const renderMenuLst = menus.filter((menu) => menu.renderClaim === undefined || claims.hasClaim(menu.renderClaim));
        return renderMenuLst;
    }

    render() {
        var renderMenuList = this._getRenderMenuItems();
        const imgSrc = 'Areas/Portal/www/dist/imgs/i_domain.jpg';
        const firstMenu = renderMenuList[0];
        return (
            <Router forceRefresh>
                <MenuList img={imgSrc}
                    menus={renderMenuList.map((menu) => { return { label: menu.label, href: menu.href } })} />
                <Suspense fallback={null}>
                    <Switch>
                        {renderMenuList.map((menu, index) => <Route key={`route_${index}`} path={menu.href} component={menu.component} />)}
                        {firstMenu && <Redirect from="/" to={firstMenu.href} />}
                    </Switch>
                </Suspense>
            </Router>
        )
    }
}

ReactDOM.render(<App />, document.getElementById("root"));
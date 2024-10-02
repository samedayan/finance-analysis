import { NavItem } from './nav-item/nav-item';

export const navItems: NavItem[] = [
  {
    navCap: 'Home',
  },
  {
    displayName: 'Dashboard',
    iconName: 'layout-dashboard',
    route: '/dashboard',
  },
  {
    navCap: 'Menu',
  },
  {
    displayName: 'Partners',
    iconName: 'user',
    route: '/ui-components/partners',
  },
  {
    displayName: 'Agreements',
    iconName: 'poker-chip',
    route: '/ui-components/agreements',
  },
  {
    displayName: 'WorkItems',
    iconName: 'rosette',
    route: '/ui-components/work-items',
  },
  {
    displayName: 'Audit logs',
    iconName: 'layout-navbar-expand',
    route: '/ui-components/audits',
  },
  {
    displayName: 'About',
    iconName: 'tooltip',
    route: '/ui-components/about',
  },
];

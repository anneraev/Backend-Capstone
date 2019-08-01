import React, { Component } from 'react';

export class PluginList extends Component {
  static displayName = PluginList.name;

  constructor (props) {
    super(props);
    this.state = { plugins: [], loading: true };

    fetch('api/Plugins')
      .then(response => response.json())
      .then(data => {
        this.setState({ plugins: data, loading: false });
      });
  }

  static renderForecastsTable (plugins) {
    console.log(plugins);
    return (
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>Title</th>
            <th>UserId</th>
            <th>EngineId</th>
          </tr>
        </thead>
        <tbody>
          {plugins.map(p =>
            <tr key={p.pluginId}>
              <td>{p.title}</td>
              <td>{p.userId}</td>
              <td>{p.engineId}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render () {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : PluginList.renderForecastsTable(this.state.plugins);

    return (
      <div>
        <h1>Plugins</h1>
        <p>This is a list of all plugins.</p>
        {contents}
      </div>
    );
  }
}

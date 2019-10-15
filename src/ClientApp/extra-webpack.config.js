module.exports = (webpackConfig, cliConfig) => {
  if (cliConfig.buildOptimizer) {
    // https://github.com/angular/angular-cli/issues/14033
    const loader = webpackConfig.module.rules.find(
      rule =>
        rule.use &&
        rule.use.find(
          it => it.loader === "@angular-devkit/build-optimizer/webpack-loader"
        )
    );
    if (!loader) {
      return webpackConfig;
    }

    const originalTest = loader.test;
    loader.test = file => {
      const isMonaco = !!file.match("node_modules/monaco-editor");
      return !isMonaco && !!file.match(originalTest);
    };
  }
  return webpackConfig;
};

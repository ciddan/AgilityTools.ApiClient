require 'rubygems'
require 'albacore'
require 'version_bumper'
require 'fileutils'

task :default => [:assemblyinfo, :compile, :test]

desc "Generate assembly info"
assemblyinfo :assemblyinfo do |asm|
  Rake::Task["bump:build"].invoke

  asm.namespaces 'System.Runtime.CompilerServices'
  asm.version = bumper_version.to_s
  asm.company_name = "Jula AB"
  asm.product_name = "Agility API Client"
  asm.title = "AgilityTools.ApiClient"
  asm.description = "A common set of tools for communicating with the Agility Adsml WebServices Api."
  asm.copyright = "Copyright (c) Jula AB 2011"
  asm.output_file = "AgilityTools.ApiClient.Adsml.Client/Properties/AssemblyInfo.cs"
  asm.custom_attributes :InternalsVisibleTo => "AgilityTools.ApiClient.Adsml.Client.Tests"
end

desc "Compile the solution"
msbuild :compile do |msb|
	rm_rf "AgilityTools.ApiClient.Adsml.Client/bin/Release/"
	mkdir_p "AgilityTools.ApiClient.Adsml.Client/bin/Release/"

	msb.properties :configuration => :Release
	msb.targets :Clean, :Build
	msb.solution = "AgilityTools.ApiClient.sln"
end

desc "Run tests"
nunit :test do |nunit|
	nunit.command = "../tools/nunit/nunit-console.exe"
	nunit.assemblies "AgilityTools.ApiClient.Adsml.Client.Tests/bin/Release/AgilityTools.ApiClient.Adsml.Client.Tests.dll", "AgilityTools.ApiClient.Adsml.Communication.Tests/bin/Release/AgilityTools.ApiClient.Adsml.Communication.Tests.dll"
	nunit.options '/framework=v4.0.30319'
end